---
uid: openxr-project-validation
---
# Validate your project

The OpenXR package defines a set of rules for the Project Validation system. These rules check for possible incompatibilities between OpenXR and the project configuration.

Some of the rules serve as warnings for possible configuration problems; you are not required to fix these. Other rules flag configuration errors that would result in your app failing to build or not working once built. You must fix these errors.

If you have the [XR Core Utilities](https://docs.unity3d.com/Packages/com.unity.xr.core-utils@latest) 2.1.0 or later installed, you can access the Project Validation status in the **Project Settings** window under **XR Plug-in Management** (menu: **Edit &gt; Project Settings**). These results include the checks for all XR plug-ins that provide validation rules.

![project-validation](images/ProjectValidation/project-validation-core-utils.png)

You can also open a separate **OpenXR Project Validation** window for OpenXR (menu: **Window &gt; XR &gt; OpenXR &gt; Project Validation**). This window only shows the validation results related to the OpenXR plug-in and features.

![feature-validation](images/ProjectValidation/feature-validation.png)

Rules that pass validation are not shown unless you enable **Show all**.

Some rules provide a **Fix** button that updates the configuration so that the rule passes validation. Other rules provide an **Edit** button that takes you to the relevant setting so that you can make the necessary adjustments yourself.

You can enable **Ignore build errors** to bypass the pre-build validation check. However, any misconfigured features in your app might not work at runtime.

### Validation issues reported in XR Plug-in Management

![loader-with-issues](images/ProjectValidation/loader-with-issues.png)

Clicking on either the validation warning or the error icon brings up the Validation window.

### Validation issues reported in features pane

![features-with-issues](images/ProjectValidation/features-with-issues.png)

Clicking on either the validation warning or the error icon brings up the Validation window.

### Validation issues reported in build

![build-with-issues](images/ProjectValidation/build-with-issues.png)

Double-clicking on build warnings or errors from validation brings up the Validation window.