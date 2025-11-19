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
          // Only chunk large vendor libraries, let Vite handle React automatically
          if (id.includes('node_modules')) {
            // Large UI component library
            if (id.includes('@radix-ui')) {
              return 'radix-ui';
            }

            // Large icon library
            if (id.includes('lucide-react')) {
              return 'icons-vendor';
            }

            // Large table library
            if (id.includes('@tanstack/react-table')) {
              return 'table-vendor';
            }

            // Group smaller vendor libraries together
            // Don't manually chunk React - let Vite handle it to avoid loading order issues
            if (
              !id.includes('react') &&
              !id.includes('react-dom') &&
              !id.includes('react-router') &&
              !id.includes('react-redux') &&
              !id.includes('react-hook-form') &&
              !id.includes('react-toastify') &&
              !id.includes('react-error-boundary')
            ) {
              return 'vendor';
            }
          }
        },
      },
    },
    chunkSizeWarningLimit: 1000, // Increase limit to 1MB for better visibility
  },
});
