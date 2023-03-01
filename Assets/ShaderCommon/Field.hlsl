#ifndef HLSL_INCLUDE_FIELD
#define HLSL_INCLUDE_FIELD

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

float4 Laplacian(const sampler2D tex, const float2 texelSize, const float2 uv)
{
    float4 sum = 0.0;
    sum += tex2D(tex, uv) * -1.0;
    sum += tex2D(tex, uv + float2(-texelSize.x, 0)) * 0.2;
    sum += tex2D(tex, uv + float2(texelSize.x, 0)) * 0.2;
    sum += tex2D(tex, uv + float2(0, -texelSize.y)) * 0.2;
    sum += tex2D(tex, uv + float2(0, texelSize.y)) * 0.2;
    sum += tex2D(tex, uv + float2(-texelSize.x, -texelSize.y)) * 0.05;
    sum += tex2D(tex, uv + float2(-texelSize.x, texelSize.y)) * 0.05;
    sum += tex2D(tex, uv + float2(texelSize.x, -texelSize.y)) * 0.05;
    sum += tex2D(tex, uv + float2(texelSize.x, texelSize.y)) * 0.05;
    
    return sum;
}

#endif