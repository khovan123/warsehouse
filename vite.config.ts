import path from 'path';

import tailwindcss from '@tailwindcss/vite';
import react from '@vitejs/plugin-react';
import { defineConfig } from 'vite';

// https://vite.dev/config/
export default defineConfig({
  plugins: [react(), tailwindcss()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, 'src'),
    },
  },
  server: {
    port: 8080,
    open: true,
    proxy: {
      '/api': {
        target: 'http://127.0.0.1:53889',
        changeOrigin: true,
        secure: false,
      },
    },
  },
  build: {
    rollupOptions: {
      output: {
        manualChunks: (id) => {
          // Vendor chunk for node_modules
          if (id.includes('node_modules')) {
            // React and React DOM
            if (id.includes('react') || id.includes('react-dom')) {
              return 'react-vendor';
            }

            // Radix UI components
            if (id.includes('@radix-ui')) {
              return 'radix-ui';
            }

            // Redux ecosystem
            if (
              id.includes('@reduxjs/toolkit') ||
              id.includes('react-redux') ||
              id.includes('redux-persist') ||
              id.includes('redux-saga')
            ) {
              return 'redux-vendor';
            }

            // React Router
            if (id.includes('react-router')) {
              return 'router-vendor';
            }

            // UI libraries
            if (id.includes('@tanstack/react-table')) {
              return 'table-vendor';
            }

            if (id.includes('lucide-react')) {
              return 'icons-vendor';
            }

            if (id.includes('react-hook-form')) {
              return 'form-vendor';
            }

            if (id.includes('axios')) {
              return 'http-vendor';
            }

            if (id.includes('react-toastify')) {
              return 'toast-vendor';
            }

            // Tailwind and styling
            if (
              id.includes('tailwind') ||
              id.includes('clsx') ||
              id.includes('class-variance-authority') ||
              id.includes('tailwind-merge')
            ) {
              return 'styles-vendor';
            }

            // Other node_modules
            return 'vendor';
          }
        },
      },
    },
    chunkSizeWarningLimit: 1000, // Increase limit to 1MB for better visibility
  },
});
