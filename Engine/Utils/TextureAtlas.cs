using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace FlyEngine;

public class TextureAtlas
{
	private Dictionary<string, TextureRegion> _regions;

	public Texture2D Texture;

	public TextureAtlas(Texture2D texture)
	{
		Texture = texture;
		_regions = new();
	}

	public void AddRegion(string name, int x, int y, int w, int h)
	{
		_regions.Add(name, new TextureRegion(Texture, x, y, w, h));
	}

	public TextureRegion GetRegion(string name)
	{
		if(!_regions.ContainsKey(name)) throw new KeyNotFoundException($"FlyEngine :: TextureAtlas.GetRegion() atlas don't have the key: {name}");

		return _regions[name];
	}

	public Sprite CreateSprite(string name)
	{
		if(!_regions.ContainsKey(name)) throw new KeyNotFoundException($"FlyEngine :: TextureAtlas.CreateSprite() atlas don't have the key: {name}");

		return new Sprite(_regions[name]);
	}

	public static TextureAtlas CreateFromFile(ContentManager content, string filename)
	{
		TextureAtlas atlas;

		string filepath = Path.Combine(content.RootDirectory, filename);

		XDocument doc = XDocument.Load(filepath);
		XElement root = doc.Root;

		string texturePath = root.Element("Texture").Value;
		atlas = new(content.Load<Texture2D>(texturePath));

		var regions = root.Element("Regions").Elements("Region");

		if(regions != null)
		{
			foreach(var region in regions)
			{
				atlas.AddRegion(
						region.Attribute("name").Value,
						int.Parse(region.Attribute("x")?.Value ?? "0"),
						int.Parse(region.Attribute("y")? .Value ?? "0"),
						int.Parse(region.Attribute("width")?.Value ?? "0"),
						int.Parse(region.Attribute("height")?.Value ?? "0")
						);
			}
		}

		return atlas;
	}
}
