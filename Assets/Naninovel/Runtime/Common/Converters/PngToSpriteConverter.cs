using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Converts <see cref="T:byte[]"/> raw data of a .png image to <see cref="Sprite"/>.
    /// </summary>
    public class PngToSpriteConverter : IRawConverter<Sprite>
    {
        public RawDataRepresentation[] Representations { get; } = {
            new RawDataRepresentation(".png", "image/png")
        };

        public Sprite Convert (byte[] obj, string name)
        {
            var texture = new Texture2D(2, 2);
            texture.name = name;
            texture.LoadImage(obj, true);
            var rect = new Rect(0, 0, texture.width, texture.height);
            var sprite = Sprite.Create(texture, rect, Vector2.one * .5f);
            return sprite;
        }

        public UniTask<Sprite> ConvertAsync (byte[] obj, string name) => UniTask.FromResult(Convert(obj, name));

        public object Convert (object obj, string name) => Convert(obj as byte[], name);

        public async UniTask<object> ConvertAsync (object obj, string name) => await ConvertAsync(obj as byte[], name);
    }
}
