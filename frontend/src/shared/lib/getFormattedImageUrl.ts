type ImageSize = "1080p" | "720p" | "big" | "small" | "med";

const sizeMap: Record<ImageSize, string> = {
  "1080p": "t_1080p",
  "720p": "t_720p",
  big: "t_cover_big",
  small: "t_cover_small",
  med: "t_cover_med",
};

export const getFormattedImageUrl = (
  imageUrl: string,
  size: ImageSize = "1080p",
  retina: boolean = false
): string => {
  const validSize = sizeMap[size] || sizeMap["med"];

  const finalSize = retina ? `${validSize}_2x` : validSize;

  return imageUrl.replace(/t_\w+/, finalSize);
};
