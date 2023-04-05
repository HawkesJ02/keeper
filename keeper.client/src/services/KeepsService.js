import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
class KeepsService{
  async getKeeps(){
    const res = await api.get('api/keeps');
    logger.log('[gotten keeps]', res.data)
    AppState.keeps = res.data
  }
  async createKeep(keepData){
    const res = await api.post('api/keeps', keepData)
    logger.log('[Attempting to create keep]', res.data)
    AppState.keeps.push(res.data)
    return res.data
  }

  async get_keep_by_id(id){
    const res = await api.get('api/keeps/' + id)
    logger.log('activekeep', res.data)
    AppState.activekeep = res.data
  }

  async delete_keep(id){
    const res = await api.delete('api/keeps/' + id)
  }

  async addKeepToVault(keepData){
    const res = await api.post('api/vaultkeeps', keepData)
    logger.log('Attempting to make vaultkeep!', res.data)
  }
}

export const keepsService = new KeepsService()