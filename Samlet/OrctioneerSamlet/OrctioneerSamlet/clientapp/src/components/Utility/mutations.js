export default {
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
        if (Array.isArray(bids) && bids.length) {
            bids.sort(function (bidA, bidB) {
                return bidB.Bid - bidA.Bid
            })
        }
        else {
            bids = [{ Bid: 0 }]
        }

        state.currentBids = bids;
    },
    BID_SUCCES(state, result) {
        state.bidSubmitted = result;
        state.currentBidTableKey += 1;
    },

    //Search
    UPDATE_SEARCH_WORD(state, word) {
        state.search = word;
        state.UpdateSortedAuctions += 1;
    },
    //UPDATE_SORT_ORDER(state, order) {
    //    state.sortOrders = order;
    //},
    UPDATE_SORT_BY(state, sort) {
        state.sortBy = sort;
        state.UpdateSortedAuctions += 1;
    }
}