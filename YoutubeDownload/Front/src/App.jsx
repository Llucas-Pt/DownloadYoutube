/* eslint-disable no-undef */
import { useState } from "react";
import './Youtube.css'



function App() {
    const [url, setUrl] = useState("");
    const [format, setFormat] = useState("mp4");
    //const [downloadLink, setDownloadLink] = useState(null);
    const [isLoading, setIsLoading] = useState(false); // estado de carregamento
    const apiUrl = process.env.REACT_APP_API_URL || "http://localhost:8080";

    

    const handleDownload = async () => {
        try {

            setIsLoading(true); // ativa o “Carregando...”

            const response = await fetch(`${apiUrl}/api/YoutubeDownload`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ url, format }),
        });

        if (!response.ok) {
            alert("Erro ao baixar o vídeo");
            return;
        }

        // Transforma a resposta (arquivo binário) em um blob
        const blob = await response.blob();
        // Cria uma URL temporária para o arquivo
        const downloadUrl = URL.createObjectURL(blob);
        // Cria um elemento <a> invisível para iniciar o download
        const link = document.createElement("a");
        link.href = downloadUrl;
        // Sugere um nome padrão de arquivo (ou deixa vazio para usar o nome do servidor)
        link.download = `download.${format}`;


            // Adiciona o link ao DOM, clica nele e remove em seguida
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);

            // Libera o objeto da memória
            URL.revokeObjectURL(downloadUrl);
        } catch (error) {
            console.error("Erro ao baixar:", error);
            alert("Ocorreu um erro ao tentar baixar o vídeo.");
        } finally {
            setIsLoading(false); // desativa o “Carregando...” depois que termina
        }
    };
    return (
        <div className="container">

            <div className="card">
                <input
                    type="text"
                    placeholder="Paste YouTube Video URL Here"
                    value={url}
                    onChange={(e) => setUrl(e.target.value)}
                    className="url-input"
                />

                <div className="toggle-buttons">
                    <button className={format === "mp4" ? "active" : ""} onClick={() => setFormat("mp4")}>
                        <span>🎬</span>
                        Video (MP4)
                    </button>
                    <button className={format === "mp3" ? "active" : ""} onClick={() => setFormat("mp3")}>
                        <span>🎵</span>
                        Audio (MP3)
                    </button>
                </div>

                <button className="download-btn" onClick={handleDownload} disabled={isLoading}>
                    {isLoading ? (
                        <>
                            <span className="spinner"></span> Baixando...
                        </>
                    ) : (
                        "Download"
                    )}
                </button>
            </div>


            <div className="history-card">
                <h2>Download History</h2>
                <table>
                    <thead>
                        <tr>
                            <th>URL</th>
                            <th>Format</th>
                            <th>Date</th>
                            <th>Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>youtube.com/watch?v=123...</td>
                            <td>MP4</td>
                            <td>2023-10-26</td>
                            <td className="success">Completed</td>
                        </tr>
                        <tr>
                            <td>youtube.com/watch?v=456...</td>
                            <td>MP4</td>
                            <td>2023-10-25</td>
                            <td className="success">Completed</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td className="error">Error</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    );
}

export default App;

