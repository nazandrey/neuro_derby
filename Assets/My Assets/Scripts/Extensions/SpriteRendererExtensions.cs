using UnityEngine;

namespace NeuroDerby.Extensions
{
    public static class SpriteRendererExtensions
    {
        public static void ChangeAlpha(this SpriteRenderer spriteRenderer, float newAlphaValue) {
            var newColor = spriteRenderer.color;
            newColor.a = newAlphaValue;
            spriteRenderer.color = newColor;
        }
    }
}