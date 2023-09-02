import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import mkcert from 'vite-plugin-mkcert'

// https://vitejs.dev/config/
export default defineConfig({
    server: {
        https: true,
    },
    plugins: [vue(), mkcert()],
    build: {
        rollupOptions: {
            output: {
                manualChunks: {
                    bundle1: ['lodash', "core-js"]
                }
            },
        },
    },
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url)),
            'vue$': 'vue/dist/vue.esm.js'
        }
    }
})
