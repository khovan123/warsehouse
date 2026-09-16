/** @type {import('tailwindcss').Config} */
import tailwindcssAnimate from 'tailwindcss-animate';
// import twAnimateCss from 'tw-animate-css';

const config = {
  content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
  plugins: [
    tailwindcssAnimate,
    // twAnimateCss
  ],
};

export default config;
