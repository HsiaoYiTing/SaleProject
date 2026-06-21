import { defineStore } from 'pinia'
import type { Member } from '@/models/Member'

export const useMemberStore = defineStore(
  'member',
  {
    state: () => ({
      member: null as Member | null,
      isLogin: false
    }),

    actions: {

      loadMember() {

        const memberStr = localStorage.getItem('member')

        if (!memberStr) { return }

        this.member = JSON.parse(memberStr)
        this.isLogin = true
      },

      login(member: Member) {
        this.member = member
        this.isLogin = true

        localStorage.setItem('member', JSON.stringify(member)) 
      },

      logout() {
        this.member = null
        this.isLogin = false

        localStorage.removeItem('member')
      }
    }
  }
)