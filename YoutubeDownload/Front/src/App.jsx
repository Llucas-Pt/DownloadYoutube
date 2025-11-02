// eslint-disable no-undef 
import { useState} from "react";
import './Youtube.css'



function App() {
    const [url, setUrl] = useState("");
    const [format, setFormat] = useState("mp4");
    //const [downloadLink, setDownloadLink] = useState(null);
    const [isLoading, setIsLoading] = useState(false); // estado de carregamento
    const apiUrl = import.meta.env.VITE_API_URL;
    const [dados, setDados] = useState([])


    const handleDownload = async () => {
        try {

            setIsLoading(true); // ativa o “Carregando...”

            const response = await fetch(`${apiUrl}/api/YoutubeDownload`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ url, format }),
        });

        if (!response.ok) {
            throw new Error("Erro ao baixar o vídeo");
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


            const dadosDownload = {
                id: Date.now(),// id único baseado no timestamp
                url: url, // Url
                format: format.toUpperCase(), // formato em maiúsculo (MP4 ou MP3)
                date: new Date().toLocaleString(), // data e hora atual
                status: "Concluído", // status do download
            };

            setDados([dadosDownload, ...dados]); // adiciona no início da tabela

            // Limpa o input
            setUrl("");

            // Libera o objeto da memória
            URL.revokeObjectURL(downloadUrl);

        } catch (error) {
            setDados(prevDados => [
                {
                    id: Date.now(), 
                    url, 
                    format: format.toUpperCase(),
                    date: new Date().toLocaleString(),
                    status: "Erro"
                },
                ...prevDados
            ]);
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
                            <th>Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        {dados.map((item) => ( 
                            <tr key={item.id}>
                            <td>{item.url.slice(0, 25)}</td>
                            <td>{item.format}</td>
                                <td className={item.status === "Concluído" ? "success" : "error"}>{item.status}</td>
                        </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}

export default App;

