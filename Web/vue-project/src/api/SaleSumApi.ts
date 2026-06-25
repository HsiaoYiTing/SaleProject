import type { SaleSummaryRequest } from '@/models/SaleSummaryRequest';
import type { SaleSumRequest } from '@/models/SaleSumRequest';
import type { SaleSumResponse } from '@/models/SaleSumResponse';
import axios from "axios"

const BASR_URL = 'http://localhost:5165/api/';
const SUM_URL = `${BASR_URL}sum/`;

export async function findByConditions(storeId: string, startDate?: string, endDate?: string): Promise<SaleSumResponse> {

    const request: SaleSumRequest = {
        storeId: storeId
    }

    if (startDate != null && startDate.length > 0) {
        request.startDate = startDate;
    }
    if (endDate != null && endDate.length > 0) {
        request.endDate = endDate;
    }

    console.log('JSON = ' + JSON.stringify(request))

    try {
        const response = await axios.post(`${SUM_URL}findbyconditions`, request)

        return response.data

    } catch (error) {

        console.error(error)

        return parseError(error)
    }
}

export async function summary(storeId: string, date: string): Promise<SaleSumResponse> {

    const request: SaleSummaryRequest = {
        storeId: storeId,
        date: date
    }

    try {
        const response = await axios.post(`${SUM_URL}summary`, request)

        return response.data

    } catch (error) {

        console.error(error)

        return parseError(error)
    }
}

export async function exportExcel(storeId: string, startDate?: string, endDate?: string) {

    const request: SaleSumRequest = {
        storeId: storeId
    }

    if (startDate != null && startDate.length > 0) {
        request.startDate = startDate;
    }
    if (endDate != null && endDate.length > 0) {
        request.endDate = endDate;
    }

    const response = await axios.post(`${SUM_URL}export`, request,
    {
        responseType: 'blob'    
    })

    const today = new Date().toISOString().split('T')[0]

    const url = window.URL.createObjectURL(response.data)
    const link = document.createElement('a')
    link.href = url
    link.download = `Sales_${today}.xlsx`
    link.click()
}

function parseError(error: unknown): SaleSumResponse {

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