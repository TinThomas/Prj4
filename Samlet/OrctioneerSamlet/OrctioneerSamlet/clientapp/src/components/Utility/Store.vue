<script>
    import Vue from "vue";
    import Vuex from "vuex";
    import axios from 'axios';
    
    Vue.use(Vuex);
    export default new Vuex.Store({
        state:{
            logged: false,

            auctions: [],
            popAuctions: [],
            newAuctions: [],
            expAuctions: [],
            updateListings: 0,

            picturePath: "",
            auctionSubmitted: false,

            currentAuction: [],
            currentPictureUrl: "",
            currentBids: [],
            bidSubmitted: false,
            currentBidTableKey: 0,
            
        },
        getters: {
            getAuctionById: (state) => (id) => {
                return state.auctions.find(auction => auction.ItemId === id)
            },
            getPopularAuctions: (state) => {
                var auctions = state.auctions;
                if (auctions != null) {
                    return auctions.sort(function (aucA, aucB) {
                        return aucB.Bids.length - aucA.Bids.length
                    })
                }
                else {
                    return "error";
                }
            },
            getNewAuctions: (state) => {
                var auctions = state.auctions.slice();
                if (auctions != null) {
                    return auctions.sort(function (aucA, aucB) {
                        return new Date(aucB.DateCreated) - new Date(aucA.DateCreated)
                    })
                }
            },
            getExpAuctions: (state) => {
                var auctions = state.auctions.slice();
                if (auctions != null) {
                    return auctions.sort(function (aucA, aucB) {
                        return new Date(aucA.ExpirationDate) - new Date(aucB.ExpirationDate)
                    })
                }
            },
            getHighestBid: () => (bids) => {
                if (Array.isArray(bids) && bids.length) {
                    bids.sort(function (bidA, bidB) {
                        return bidB.Bid - bidA.Bid
                    })
                    return bids[0].Bid + " Gold";
                }
                else {
                    return "No bids";
                }
            },
            getCurrentHighestBid: (state) => {
                if (Array.isArray(state.currentBids) && state.currentBids.length) {
                    return state.currentBids[0].Bid + " Gold";
                }
                else {
                    return "No bids";
                }
            }
        },
        mutations:{
            login: state => state.logged = true,
            logout: state => state.logged = false,

            //Auctions lists
            SAVE_AUCTIONS(state, auctions) {
                state.auctions = auctions;
            },
            SAVE_POP_AUCTIONS(state, auctions) {
                state.popAuctions = auctions;
            },
            SAVE_NEW_AUCTIONS(state, auctions) {
                state.newAuctions = auctions;
            },
            SAVE_EXP_AUCTIONS(state, auctions) {
                state.expAuctions = auctions;
            },
            UPDATE_LISTING(state) {
                state.updateListing += 1;
            },

            //Add Auction
            SAVE_NEW_PICTURE(state, picture) {
                state.picturePath = picture;
            },
            UPLOAD_SUCCES(state, result) {
                state.auctionSubmitted = result;
            },

            // Current auction
            SAVE_CURRENT_AUCTION(state, auction) {
                state.currentAuction = auction;
            },
            SAVE_CURRENT_PICTURE_URL(state, url) {
                state.currentPictureUrl = url;
            },
            SAVE_CURRENT_BIDS(state, bids) {
                state.currentBids = bids;
            },
            BID_SUCCES(state, result) {
                state.bidSubmitted = result;
                state.currentBidTableKey += 1;
            },



        },
        actions:{
            login (context){
                context.commit('login');
            },
            logout (context){
                context.commit('logout');
            },

            //Auction lists
            async loadAuctions({ commit }) {
                await axios.get('http://localhost:5000/api/ItemEntity/item').then(function (response) {
                    if (response.status != 200) {
                        window.console.log(response.status)
                    }
                    commit('SAVE_AUCTIONS', response.data);
                })
            },
            async loadPopAuctions({ commit }) {
                await axios.get('http://localhost:5000/api/ItemEntity/item/pop').then(function (response) {
                    if (response.status != 200) {
                        window.console.log(response.status)
                    }
                    var popAuctions = response.data.sort(function (aucA, aucB) {
                        return aucB.Bids.length - aucA.Bids.length
                    })
                    commit('SAVE_POP_AUCTIONS', popAuctions);
                    commit('UPDATE_LISTING');
                })
            },
            async loadNewAuctions({ commit }) {
                await axios.get('http://localhost:5000/api/ItemEntity/item/').then(function (response) {
                    if (response.status != 200) {
                        window.console.log(response.status)
                    }
                    var newAuctions = response.data.sort(function (aucA, aucB) {
                        return new Date(aucB.DateCreated) - new Date(aucA.DateCreated)
                    })
                    commit('SAVE_NEW_AUCTIONS', newAuctions);
                    commit('UPDATE_LISTING');
                })
                
            },
            async loadExpAuctions({ commit }) {
                await axios.get('http://localhost:5000/api/ItemEntity/item/').then(function (response) {
                    if (response.status != 200) {
                        window.console.log(response.status)
                    }
                    var expAuctions = response.data.sort(function (aucA, aucB) {
                        return new Date(aucA.ExpirationDate) - new Date(aucB.ExpirationDate)
                    })
                    commit('SAVE_EXP_AUCTIONS', expAuctions);
                    commit('UPDATE_LISTING');
                })
            },
            updateListing(context) {
                context.commit('UPDATE_LISTING');
            },

            //Add auction
            async uploadFile({ commit }, payload) {
                payload.formData.append('file', payload.selectedFile, payload.selectedFile.name)
                await axios.post('http://localhost:5000/api/ItemEntity/CreateImage', payload.formData)
                    .then(function (response) {
                        if (response.status != 200) {
                            window.console.log(response.status)
                        }

                        commit('SAVE_NEW_PICTURE', response.data)

                    }).catch(function (error) {
                        window.console.log(error)
                    });
            },
            async postNewAuction({ commit }, {title, buyOutPrice, description, expDate, picturePath }) {
                await axios.post('http://localhost:5000/api/ItemEntity/Item',
                    {
                        Title: title,
                        BuyOutPrice: buyOutPrice,
                        DescriptionOfItem: description,
                        ExpirationDate: expDate,
                        Image: picturePath
                    }).then(function (response) {
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
                await axios.get('http://localhost:5000/api/ItemEntity/item/' + payload.id).then(function (response) {
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
                await axios.get('http://localhost:5000/api/BidEntities/GetBidsFromItem/' + payload.id).then(function (response) {
                    if (response.status != 200) {
                        window.console.log(response.status)
                    }
                    var bids = response.data;
                    if (Array.isArray(bids) && bids.length) {
                        bids.sort(function (bidA, bidB) {
                            return bidB.Bid - bidA.Bid
                        })
                    }
                    else {
                        bids = [{Bid: 0}]
                    }
                    commit('SAVE_CURRENT_BIDS', bids);
                })
            },

            async postNewBid({ commit }, {bid, userIdBuyer, itemId}) {
                await axios.post('http://localhost:5000/api/BidEntities/newBid',
                    {
                        Bid: bid,
                        UserIdBuyer: userIdBuyer,
                        itemId: itemId
                    }).then(function (response) {
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
        }
    });
</script>
