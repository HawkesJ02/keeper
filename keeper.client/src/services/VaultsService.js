import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
class VaultsService{
  async createVault(vaultData){
    const res = await api.post('api/vaults', vaultData)
    logger.log('[Attempting to create vault]', res.data)
    return res.data
  }
  async get_vault_by_id(id){
    const res = await api.get('api/vaults/' + id)
    logger.log('gotten vault', res.data)
    AppState.vaults = res.data
  }

  async get_keeps_by_vault(id){
    const res = await api.get('api/vaults/' + id + '/keeps')
    logger.log('gotten keeps by vault:', res.data)
    AppState.keeps = res.data
  }
}

export const vaultsService = new VaultsService();