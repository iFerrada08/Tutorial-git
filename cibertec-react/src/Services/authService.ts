import axios from "axios";

/** Esta funcion sirve para obtener un nuevo token de un usuario */
export async function getToken(email: string, password: string): Promise<string> {
    const axiosResponse = await axios.post(`${process.env.REACT_APP_AUTH_URL}/account/token`, {
        email, password
    });
    return axiosResponse.data.token;
}