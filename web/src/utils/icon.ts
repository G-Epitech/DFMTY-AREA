export function iconName(iconId: string) {
  const words = iconId.split('-');

  const camelCase = words
    .map((word, index) =>
      index === 0 ? word : word.charAt(0).toUpperCase() + word.slice(1)
    )
    .join('');

  return `hero${camelCase.charAt(0).toUpperCase() + camelCase.slice(1)}Solid`;
}
