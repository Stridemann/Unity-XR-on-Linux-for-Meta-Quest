#include "plugin_load.h"
#include <cstdlib>
#include <cstring>
#include <string>

#if defined(XR_USE_PLATFORM_WIN32) && !defined(XR_USE_PLATFORM_UWP)

PluginHandle Plugin_LoadLibrary(const wchar_t* libName)
{
    std::wstring lib(libName);
    if ((lib.size() >= 1 && lib[0] == L'.') ||
        lib.find(L'/') == std::string::npos && lib.find(L'\\') == std::string::npos)
    {
        // Look up path of current dll
        wchar_t path[MAX_PATH];
        HMODULE hm = NULL;
        if (GetModuleHandleEx(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
                    GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
                (LPCWSTR)&Plugin_LoadLibrary, &hm) == 0)
        {
            int ret = GetLastError();
            fprintf(stderr, "GetModuleHandle failed, error = %d\n", ret);
            return NULL;
        }
        if (GetModuleFileNameW(hm, path, MAX_PATH) == 0)
        {
            int ret = GetLastError();
            fprintf(stderr, "GetModuleFileNameW failed, error = %d\n", ret);
            return NULL;
        }

        std::wstring basePath(path);
        basePath = basePath.substr(0, basePath.find_last_of(L'\\') + 1);

        lib = basePath + lib + L".dll";
    }

    HMODULE handle = LoadLibraryW(lib.c_str());
    if (handle == NULL)
    {
        int ret = GetLastError();
        fprintf(stderr, "LoadLibraryW failed, error = %d\n", ret);
    }
    return handle;
}

void Plugin_FreeLibrary(PluginHandle handle)
{
    FreeLibrary(handle);
}

PluginFunc Plugin_GetSymbol(PluginHandle handle, const char* symbol)
{
    return GetProcAddress(handle, symbol);
}

#elif defined(XR_USE_PLATFORM_UWP)

PluginHandle Plugin_LoadLibrary(const wchar_t* libName)
{
    if (libName == NULL)
        return NULL;
    HMODULE handle = LoadPackagedLibrary(libName, 0);
    if (handle == NULL)
    {
        int ret = GetLastError();
        fprintf(stderr, "LoadPackagedLibrary failed, error = %d\n", ret);
    }
    return handle;
}

void Plugin_FreeLibrary(PluginHandle handle)
{
    FreeLibrary(handle);
}

PluginFunc Plugin_GetSymbol(PluginHandle handle, const char* symbol)
{
    return GetProcAddress(handle, symbol);
}

#else // Posix

PluginHandle Plugin_LoadLibrary(const wchar_t* libName)
{
    std::wstring lib(libName);
    std::string mbLibName;

#if !defined(XR_USE_PLATFORM_OSX)
    size_t len = std::wcstombs(nullptr, lib.c_str(), lib.size());
    if (len <= 0)
        return NULL;
    mbLibName.resize(len);
    std::wcstombs(&mbLibName[0], lib.c_str(), lib.size());
#endif

#if defined(XR_USE_PLATFORM_OSX)
    mbLibName = Convert_utf16_to_utf8(libName);
#endif

    if ((lib.size() >= 1 && lib[0] == L'.') ||
        (lib.find(L'/') == std::string::npos && lib.find(L'\\') == std::string::npos))
    {
        Dl_info info;
        if (dladdr((const void*)&Plugin_LoadLibrary, &info) != 0)
        {
            std::string basePath(info.dli_fname);
            basePath = basePath.substr(0, basePath.find_last_of('/') + 1);

#if !defined(XR_USE_PLATFORM_OSX)
            if (mbLibName[0] != '.')
                mbLibName = basePath + "lib" + mbLibName;
            else
#endif
                mbLibName = basePath + mbLibName;
#if defined(XR_USE_PLATFORM_OSX)
            mbLibName += ".dylib";
#else
            mbLibName += ".so";
#endif
        }
    }
    if (mbLibName.size() <= 0)
        return NULL;

    auto ret = dlopen(mbLibName.c_str(), RTLD_LAZY);
    return ret;
}

void Plugin_FreeLibrary(PluginHandle handle)
{
    dlclose(handle);
}

PluginFunc Plugin_GetSymbol(PluginHandle handle, const char* symbol)
{
    return dlsym(handle, symbol);
}

// Function to convert a single UTF-16 code point to UTF-8 bytes
void Convert_utf16_to_utf8_char(char32_t utf16_codepoint, std::string& utf8_output)
{
    if (utf16_codepoint < 0x80)
    {
        // 1-byte sequence
        utf8_output += static_cast<char>(utf16_codepoint);
    }
    else if (utf16_codepoint < 0x800)
    {
        // 2-byte sequence
        utf8_output += static_cast<char>(0xC0 | (utf16_codepoint >> 6));
        utf8_output += static_cast<char>(0x80 | (utf16_codepoint & 0x3F));
    }
    else if (utf16_codepoint < 0x10000)
    {
        // 3-byte sequence
        utf8_output += static_cast<char>(0xE0 | (utf16_codepoint >> 12));
        utf8_output += static_cast<char>(0x80 | ((utf16_codepoint >> 6) & 0x3F));
        utf8_output += static_cast<char>(0x80 | (utf16_codepoint & 0x3F));
    }
    else if (utf16_codepoint < 0x110000)
    {
        // 4-byte sequence
        utf8_output += static_cast<char>(0xF0 | (utf16_codepoint >> 18));
        utf8_output += static_cast<char>(0x80 | ((utf16_codepoint >> 12) & 0x3F));
        utf8_output += static_cast<char>(0x80 | ((utf16_codepoint >> 6) & 0x3F));
        utf8_output += static_cast<char>(0x80 | (utf16_codepoint & 0x3F));
    }
}

std::string Convert_utf16_to_utf8(const wchar_t* libName)
{
    std::wstring utf16_string(libName);

    std::string utf8_result;
    for (size_t i = 0; i < utf16_string.length(); ++i)
    {
        char32_t codepoint = utf16_string[i];
        if (codepoint >= 0xD800 && codepoint <= 0xDBFF)
        { // High surrogate
            if (i + 1 < utf16_string.length())
            {
                char32_t next_codepoint = utf16_string[i + 1];
                if (next_codepoint >= 0xDC00 && next_codepoint <= 0xDFFF)
                { // Low surrogate
                    codepoint = 0x10000 + ((codepoint - 0xD800) << 10) + (next_codepoint - 0xDC00);
                    i++; // Consume the low surrogate
                }
            }
        }
        Convert_utf16_to_utf8_char(codepoint, utf8_result);
    }
    return utf8_result;
}
#endif
