using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Sandbox.Shaders
{
    public class AlphaToKeyEffect : ShaderEffect
    {
        private static PixelShader pixelShader =
            new PixelShader() { UriSource = makePackUri("Shaders/AlphaToKey.ps", typeof(AlphaToKeyEffect)) };

        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(AlphaToKeyEffect), 0);

        public AlphaToKeyEffect()
        {
            PixelShader = pixelShader;
            UpdateShaderValue(InputProperty);
        }

        private static Uri makePackUri(string relativeFile, Type type)
        {
            Assembly a = type.Assembly;

            // Extract the short name.
            string assemblyShortName = a.ToString().Split(',')[0];

            string uriString = "pack://application:,,,/" +
                assemblyShortName +
                ";component/" +
                relativeFile;

            return new Uri(uriString);
        }
    }
}
