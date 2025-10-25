import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
    plugins: [react()],
    server: {
        port: 3005,       //Porta
        strictPort: true, //Se a 3005 estiver ocupada, d� erro (n�o muda automaticamente)
    },
})
