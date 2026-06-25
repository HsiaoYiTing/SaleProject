import type { Sale } from './Sale'

export interface SaleResponse {

  code: number
  message: string
  data?: Sale[]
} 