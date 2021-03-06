﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SlimDX.Direct3D9;
using SlimDX;

namespace SharpWoW.UI
{
    public class Font
    {
        SlimDX.Direct3D9.Font mBaseFont;

        public Font(string familyName)
        {
            mBaseFont = new SlimDX.Direct3D9.Font(Game.GameManager.GraphicsThread.GraphicsManager.Device,
                30, 0, FontWeight.SemiBold, 1, false, CharacterSet.Ansi, Precision.TrueTypeOnly, FontQuality.Antialiased,
                PitchAndFamily.Default, familyName);

            Game.GameManager.GraphicsThread.GraphicsManager.VideoResourceMgr.AddVideoResource(new Video.VideoResource(mBaseFont.OnLostDevice, mBaseFont.OnResetDevice));
        }

        public void DrawString(Vector2 position, string text, Color color, float emSize)
        {
            Matrix matScale = Matrix.Scaling(emSize / 30.0f, emSize / 30.0f, 1);
            var sprite = FontManager.Sprite;

            var oldTransform = sprite.Transform;
            sprite.Transform = matScale * Matrix.Translation(position.X, position.Y, 0);

            mBaseFont.DrawString(sprite, text, 0, 0, color);

            sprite.Transform = oldTransform;
        }

        public Vector2 MeasureString(Vector2 position, string text, float emSize)
        {
            Matrix matScale = Matrix.Scaling(emSize / 30.0f, emSize / 30.0f, 1);
            var sprite = FontManager.Sprite;

            var oldTransform = sprite.Transform;
            sprite.Transform = matScale * Matrix.Translation(position.X, position.Y, 0);

            var rect = mBaseFont.MeasureString(sprite, text, DrawTextFormat.Center);

            sprite.Transform = oldTransform;

            var vec = new Vector2(rect.Width, rect.Height);
            return Vector2.TransformCoordinate(vec, matScale);
        }
    }
}
