using SFML.Graphics;

namespace AGWAE
{
    internal static class Sprites
    {
        private static Sprite LoadSprite(Texture texture, IntRect pos)
        {
            return new Sprite(texture, pos);
        }

        public static float GetCenterXOf(UIObject sprite)
        {
            return (Config.WINDOW_SIZE_X / 2) - (sprite.Size.X / 2);
        }

        public static float GetCenterYOf(UIObject sprite)
        {
            return (Config.WINDOW_SIZE_Y / 2) - (sprite.Size.Y / 2);
        }

        private static readonly Texture uiSmallButtons = new("Assets/ui-small-buttons.png");
        private static readonly Texture uiLargeButtons = new("Assets/ui-large-buttons-horizontal.png");
        private static readonly Texture platforms = new("Assets/Platforms.png");

        public static Sprite Play { get; } = LoadSprite(uiLargeButtons, new IntRect(96, 64, 48, 16));
        public static Sprite Quit { get; } = LoadSprite(uiLargeButtons, new IntRect(288, 64, 48, 16));
        public static Sprite Home { get; } = LoadSprite(uiSmallButtons, new IntRect(128, 96, 16, 16));

        public static Sprite SingleLeft { get; } = LoadSprite(platforms, new IntRect(0, 0, 16, 16));
        public static Sprite Single { get; } = LoadSprite(platforms, new IntRect(17, 0, 16, 16));
        public static Sprite SingleRight { get; } = LoadSprite(platforms, new IntRect(34, 0, 16, 16));
        public static Sprite TopLeft { get; } = LoadSprite(platforms, new IntRect(0, 17, 16, 16));
        public static Sprite Top { get; } = LoadSprite(platforms, new IntRect(17, 17, 16, 16));
        public static Sprite TopRight { get; } = LoadSprite(platforms, new IntRect(34, 17, 16, 16));
        public static Sprite MiddleLeft { get; } = LoadSprite(platforms, new IntRect(0, 34, 16, 16));
        public static Sprite Middle { get; } = LoadSprite(platforms, new IntRect(17, 34, 16, 16));
        public static Sprite MiddleRight { get; } = LoadSprite(platforms, new IntRect(34, 34, 16, 16));
        public static Sprite BottomLeft { get; } = LoadSprite(platforms, new IntRect(0, 51, 16, 16));
        public static Sprite Bottom { get; } = LoadSprite(platforms, new IntRect(17, 51, 16, 16));
        public static Sprite BottomRight { get; } = LoadSprite(platforms, new IntRect(34, 51, 16, 16));
        public static Sprite SlopeLeft { get; } = LoadSprite(platforms, new IntRect(0, 68, 16, 16));
        public static Sprite SlopeRight { get; } = LoadSprite(platforms, new IntRect(17, 68, 16, 16));
        public static Sprite SlopeUnderLeft { get; } = LoadSprite(platforms, new IntRect(34, 68, 16, 16));
        public static Sprite SlopeUnderRight { get; } = LoadSprite(platforms, new IntRect(0, 85, 16, 16));
        public static Sprite SlopeUnderRightWithoutBorder { get; } = LoadSprite(platforms, new IntRect(17, 85, 16, 16));
        public static Sprite SlopeUnderLeftWithoutBorder { get; } = LoadSprite(platforms, new IntRect(34, 85, 16, 16));
        public static Sprite SingleBoth { get; } = LoadSprite(platforms, new IntRect(0, 102, 16, 16));
    }
}
