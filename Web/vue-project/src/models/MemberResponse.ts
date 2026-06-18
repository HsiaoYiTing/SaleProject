import type { Member } from './Member'

export interface MemberResponse {

  code: number
  message: string
  data?: Member
} 