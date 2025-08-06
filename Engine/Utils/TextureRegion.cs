using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FlyEngine;

public class TextureRegion
{
	public int X { get { return SourceRectangle.X; } set { SourceRectangle.X = value; } }
	public int Y { get { return SourceRectangle.Y; } set { SourceRectangle.Y = value; } }
	public int Width{ get { return SourceRectangle.Width; } set { SourceRectangle.Width = value; } }
	public int Height{ get { return SourceRectangle.Height; } set { SourceRectangle.Height = value; } }
	public Texture2D Texture;
	public Rectangle SourceRectangle;

	public TextureRegion() {}

	public TextureRegion(Texture2D texture, int x, int y, int w, int h)
	{
		SourceRectangle = new Rectangle(x, y, w, h);
		Texture = texture;
	}
}
