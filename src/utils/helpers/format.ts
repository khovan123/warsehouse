const TWO_NUMBER_DECIMAL = 2;

export const formatDecimalNumber = (
  value: number,
  decimal: number = TWO_NUMBER_DECIMAL
): string => {
  return `${(Math.round(value * 100) / 100).toFixed(decimal)}`;
};
