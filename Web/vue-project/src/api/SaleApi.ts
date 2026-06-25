import type { SaleRequest } from "@/models/SaleRequest";
import type { SaleResponse } from "@/models/SaleResponse";
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


export async function findByDate(storeId: string, date: string): Promise<SaleResponse> {

    const request: SaleRequest = {
        storeId: storeId,
        date: date
    }

    try {
        const response = await axios.post(`${SALE_URL}findbyDate`, request)

        return response.data

    } catch (error) {

        console.error(error)

        return parseError(error)
    }
}

function parseError(error: unknown): SaleResponse {

    if (axios.isAxiosError(error)) {

        console.log("status = " + error.response?.status)
        console.log("statusText = " + error.response?.statusText)
        console.log("data = " + error.response?.data)

        var errorMsg = error.response?.statusText ?? ""
        if (error.code === 'ERR_NETWORK') {
            errorMsg = 'API 沒開或 Port 錯誤'
        } else  if (error.code === 'ECONNABORTED') {
            errorMsg = 'Request Timeout'
        }

        return {
            code: error.response?.status ?? 0,
            message: errorMsg
        }
    }

    if (error instanceof Error) {
        return {
            code: -999,
            message: error.message
        }
    }

    return {
        code: -9999,
        message: 'Unknown Error'

    }       
}