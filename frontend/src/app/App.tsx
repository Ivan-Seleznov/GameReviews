import { Providers } from "./providers/Providers";
import { AppRouter } from "./router";

function App() {
  const apiUrl = import.meta.env.VITE_API_URL;
  console.log("apiUrl: " + apiUrl);
  return (
    <Providers>
      <AppRouter />
    </Providers>
  );
}

export default App;
