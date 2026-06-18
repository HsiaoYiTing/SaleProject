import type { Member } from '@/models/Member'
import type { MemberResponse } from '@/models/MemberResponse'
import axios from "axios"

const BASR_URL = 'http://localhost:5165/api/';
const Member_URL = `${BASR_URL}member/`;

export async function addMember(
    account: string, 
    password: string, 
    name: string): Promise<MemberResponse> {

    const request: Member = {
        account: account,
        password: password,
        name: name
    }

    try {
        const response = await axios.post(`${Member_URL}add`, request)
        return response.data

    } catch (error) {

        console.error(error)

        return parseError(error)
    }
}

export async function login(
    account: string, 
    password: string) {

    const request: Member = {
        account: account,
        password: password,
        name: ""
    }

    try {
        const response = await axios.post(`${Member_URL}login`, request)
        return response.data

    } catch (error) {

        console.error(error)

        return parseError(error)
    }
}

function parseError(error: unknown): MemberResponse {

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