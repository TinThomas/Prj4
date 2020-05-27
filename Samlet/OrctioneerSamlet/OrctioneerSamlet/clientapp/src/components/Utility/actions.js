
import api from './api';

export default {
    login(context) {
        context.commit('login');
    },
    logout(context) {
        context.commit('logout');
    },

    //Auction lists
    async loadAuctions({ commit }) {
         await api.fetchAuctions().then(function (response) {
            if (response.status != 200) {
                window.console.log(response.status)
            }
            commit('SAVE_AUCTIONS', response.data);
            commit('UPDATE_LISTING');
        })
    },
   
    updateListing(context) {
        context.commit('UPDATE_LISTING');
    },

    //Add auction
    async uploadFile({ commit }, payload) {
        payload.formData.append('file', payload.selectedFile, payload.selectedFile.name)
        await api.postImage(payload.formData)
            .then(function (response) {
                if (response.status != 200) {
                    window.console.log(response.status)
                }

                commit('SAVE_NEW_PICTURE', response.data)

            }).catch(function (error) {
                window.console.log(error)
            });
    },
    async postNewAuction({ commit }, { title, buyOutPrice, description, expDate, picturePath }) {
        await api.postNewAuction(title, buyOutPrice, description, expDate, picturePath).then(function (response) {
                if (response.status != 200) {
                    window.console.log(response.status)
                    commit('UPLOAD_SUCCES', false)
                }
                else {
                    commit('UPLOAD_SUCCES', true)
                }
            }).catch(function (error) {
                window.console.log(error)
            });
    },
    resetUpload({ commit }) {
        commit('UPLOAD_SUCCES', false);
    },

    //Current auction
    async loadCurrentAuction({ commit }, payload) {
        await api.fetchAuctionById(payload.id).then(function (response) {
            if (response.status != 200) {
                window.console.log(response.status)
            }

            commit('SAVE_CURRENT_AUCTION', response.data);

            if (response.data.Image == null) {
                commit('SAVE_CURRENT_PICTURE_URL', require("./../../images/missingimage.jpg"));
            }
            else {
                commit('SAVE_CURRENT_PICTURE_URL', require("./../../images/" + response.data.Image));
            }
        })
    },

    async loadCurrentBids({ commit }, payload) {
        await api.fetchBidsById(payload.id).then(function (response) {
            if (response.status != 200) {
                window.console.log(response.status)
            }

            commit('SAVE_CURRENT_BIDS', response.data);
        })
    },

    async postNewBid({ commit }, { bid, userIdBuyer, itemId }) {
        await api.postNewBid(bid, userIdBuyer, itemId).then(function (response) {
                if (response.status != 200) {
                    window.console.log(response.status)
                    commit('BID_SUCCES', false)
                }
                else {
                    commit('BID_SUCCES', true)
                }
            }).catch(function (error) {
                window.console.log(error)
            });
    },
    resetBidSubmitted({ commit }) {
        commit('BID_SUCCES', false);
    },

    //Search
    updateSearchWord({ commit }, word) {
        commit('UPDATE_SEARCH_WORD', word)
    },
    updateSortBy({ commit }, sort) {
        commit('UPDATE_SORT_BY', sort)
    }
}