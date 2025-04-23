import axios, { AxiosInstance, InternalAxiosRequestConfig } from "axios";
const API_URL = import.meta.env.VITE_API_URL ?? "";

function authRequestInterceptor(config: InternalAxiosRequestConfig) {
  config.headers["Content-Type"] = "application/json";
  config.headers["Accept"] = "application/json";
  return config;
}

export const configureHttpClient = (client: AxiosInstance) => {
  client.interceptors.request.use(authRequestInterceptor);
  client.interceptors.response.use(
    (response) => {
      return response;
    },
    (error) => {
      const message = error.response?.data?.message || error.message;
      console.error("axios error: " + message);
      // TODO: Handle errors

      if (error.response?.status === 401) {
        // TODO: Handle 401
      }

      return Promise.reject(error);
    }
  );

  console.log("API_URL = ", API_URL);
  client.defaults.baseURL = API_URL;
};

const httpClient = axios.create();
configureHttpClient(httpClient);

export default httpClient;
