import axios from "axios"

const BASR_URL = 'http://localhost:5165/api/';
const SALE_URL = `${BASR_URL}sale/`;


export const importFile = async (file: File) => {

    const formData = new FormData()

    formData.append("file", file)

    const response = await axios.post(`${SALE_URL}import`, formData,
    {
        headers: {
            "Content-Type": "multipart/form-data"
        }
    })

    return response.data
}