# Composition Layer Feature Sample

Demonstrates how to add support for customized composition layer types using OpenXR.

This sample doesn't contain a sample scene. Instead it contains three scripts, as described in the following sections.

## Custom quad layer data

`CustomQuadLayerData.cs` demonstrates how to create a custom composition layer. It defines a custom layer for a custom quad shape.

## Custom feature

`CustomFeature.cs` demonstrates how to enable and manage custom composition layers in OpenXR. The script enables a handler for the customized composition layer type (`CustomLayerHandler`) when the OpenXR feature is enabled.

## Custom layer handler

`CustomLayerHandler.cs` demonstrates how to manage the lifecycle of a composition layer. This script implements a custom handler class `CustomLayerHandler` for the custom quad layer. The custom handler manages the lifecycle of the quad composition layer.
