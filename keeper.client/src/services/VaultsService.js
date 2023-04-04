import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
class VaultsService{
  async createVault(vaultData){
    const res = await api.post('api/vaults', vaultData)
    logger.log('[Attempting to create vault]', res.data)
    AppState.vaults.push(res.data)
    return res.data
  }
}

export const vaultsService = new VaultsService();