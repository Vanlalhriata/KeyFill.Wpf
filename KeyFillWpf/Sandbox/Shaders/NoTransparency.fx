sampler2D implicitInputSampler : register(S0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 color = tex2D(implicitInputSampler, uv);

	// Assuming premultiplied alpha
    color.r = color.r / color.a;
    color.g = color.g / color.a;
    color.b = color.b / color.a;

    color.a  = 1;
  
    return color;
}

