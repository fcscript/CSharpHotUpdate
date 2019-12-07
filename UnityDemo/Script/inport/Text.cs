using System;


class  Text : MaskableGraphic
{
    public TextGenerator cachedTextGenerator { get; }
    public TextGenerator cachedTextGeneratorForLayout { get; }
    public Texture mainTexture { get; }
    public Font font { get;  set; }
    public StringA text { get;  set; }
    public bool supportRichText { get;  set; }
    public bool resizeTextForBestFit { get;  set; }
    public int resizeTextMinSize { get;  set; }
    public int resizeTextMaxSize { get;  set; }
    public TextAnchor alignment { get;  set; }
    public bool alignByGeometry { get;  set; }
    public int fontSize { get;  set; }
    public HorizontalWrapMode horizontalOverflow { get;  set; }
    public VerticalWrapMode verticalOverflow { get;  set; }
    public float lineSpacing { get;  set; }
    public FontStyle fontStyle { get;  set; }
    public float pixelsPerUnit { get; }
    public float minWidth { get; }
    public float preferredWidth { get; }
    public float flexibleWidth { get; }
    public float minHeight { get; }
    public float preferredHeight { get; }
    public float flexibleHeight { get; }
    public int layoutPriority { get; }
    public void FontTextureChanged(){}
    public TextGenerationSettings GetGenerationSettings(Vector2 extents){ return default(TextGenerationSettings); }
    public static Vector2 GetTextAnchorPivot(TextAnchor anchor){ return default(Vector2); }
    public void CalculateLayoutInputHorizontal(){}
    public void CalculateLayoutInputVertical(){}
};

