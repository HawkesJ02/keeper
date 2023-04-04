import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class ProfilesService{

  async get_profile_by_id(profile_id){
    const res = await api.get('api/profiles/' + profile_id)
    logger.log(res.data)
    AppState.profile = res.data
  }

  async get_keeps_by_profile(profile_id){
    const res = await api.get('api/profiles/' + profile_id + '/keeps')
    logger.log('[gotten keeps]', res.data)
    AppState.keeps = res.data
  }

  async get_vaults_by_profile(profile_id){
    const res = await api.get('api/profiles/' + profile_id + '/vaults')
    logger.log(res.data, 'gotten vaults for profile')
    AppState.vaults = res.data
  }
}
export const profilesService = new ProfilesService();