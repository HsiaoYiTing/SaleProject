import type { SaleSum } from './SaleSum'

export interface SaleSumResponse {

  code: number
  message: string
  data?: SaleSum[]
} 