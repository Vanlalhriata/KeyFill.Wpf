sampler2D implicitInputSampler : register(S0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 color = tex2D(implicitInputSampler, uv);

    color.rgb = color.a;
    color.a = 1;
  
    return color;
}

