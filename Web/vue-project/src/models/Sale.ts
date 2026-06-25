import type { Store } from './Store'
import type { Product } from './Product'

export interface Sale {

    id: string

    store: Store

    product: Product

    price: number

    qty: number

    saleTime: string

    createTime: string

    updateBy: string

}