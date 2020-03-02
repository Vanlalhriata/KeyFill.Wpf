# Key Fill Wpf

Generate key and fill components of WPF visuals containing transparent (and translucent) parts for use in video compositing.

## Usage (tl;dr)

- Ensure the visuals to display have two levels of container ancestors.
   Example:
   ```
  <!-- FillWindow -->
  <Grid x:Name="rootPanel">
	  <Grid x:Name="fillPanel">
		  <!-- Visuals to display -->
	  </Grid>
  </Grid>
  ```
- Apply the NoTransparency shader to the top container (`rootPanel`)
- Create a Window (`KeyWindow`) and apply the AlphaToKey shader to it
- Display the second level container (`fillPanel`) in this window using `VisualBrush`

For a full example, take a look at the Sandbox project.

## The problem being addressed

If you have a WPF application where the output visuals are meant to be included as part of another video, you can use video compositing. Common scenarios include game streaming, or broadcasting of sports, news, etc.. The WPF application would produce a visual, say, a window, to be overlain on top of the other video.

The problem is that unlike images, videos do not inherently come with transparency. So, any transparent part on the WPF visual would be transmitted as black on the final video. This would not be a problem if the output was meant to be rectangular in shape anyway (although this also poses a problem depending on how the video is composited), but most of the time you would want to use transparency for better visuals.

### Alternate solution

To solve the problem of not being able to broadcast transparent parts of the WPF visual, one possible solution is to use either Colour Keying, or Luma Keying. Colour Keying (also called Chroma Keying) works by removing a certain colour range from the visual and treating it as transparent during compositing. In a similar fashion, Luma Keying works by keying out certain parts of the output visual based on their brightness or darkness.

The problem with these techniques is that the colour range or brightness range being keyed out cannot be present in the output visual, lest they be keyed out too. This is why weather reporters cannot wear green (or blue, depending on the technique).

### Key and Fill solution

To provide a cleaner solution, the output visual can be separated into two separate outputs, namely the Fill and the Key. The Fill is simply the visual output itself. The Key provides information regarding what the transparency of each pixel should be.

During video compositing, each pixel's colour comes from the Fill visual; and the alpha comes from the Key visual. This allows the visual content to contain any range of colour or brightness.

## WPF implementation

To generate Key and Fill outputs in WPF, the sample project uses simple pixel shaders.

The Fill window represents the desired output. The Key window copies the content of the Fill window using a VisualBrush (`System.Windows.Media.VisualBrush`) and applies a pixel shader called AlphaToKey. This shader replaces the colour of each pixel with the alpha, thus producing a grayscale output where white represents fully opaque and black represents fully transparent.

This is implemented in `FillWindow` and `KeyWindow` in the Sandbox project.

### Problems with translucency (alpha between 0 and 1)

While the solution above works well when the output visual contains only either fully opaque or fully transparent parts, it is not sufficient for visuals with alpha between 0 and 1, or translucent parts. Videos, not being able to store transparency, would show the translucent parts as having a black background. The solution above would still show translucency in the final result, but there would be a dark tinge because of the black background, producing undesirable results.

### Solution for translucency

To overcome the problem stated above, we use a second pixel shader and perform some XAML tweaking. These are implemented in `FillWithTranslucencyWindow` and `KeyWithTranslucencyWindow` in the Sandbox project. The implementation is explained below:

As videos cannot have transparency, any transparency in the Fill window is removed using the NoTransparency pixel shader. This shader simply forces the alpha of each pixel to 1. This way, the Fill window contains only the colour (RGB) information without any alpha information; the alpha information comes from the Key window as before. Note that this shader assumes premultiplied alpha is being used, and recalculates the RGB values accordingly.

The previous `KeyWindow` copied the output of the `FillWindow` for transparency information. But since all transparency was removed in `FillWithTranslucencyWindow`, the new Key window needs to copy the visual before the transparency-removing shader is applied. Towards this end, the new fill window has all its visuals in a `fillPanel` panel which will be copied by the new key window for the transparency information.

## License
Licensed under the MIT license.
