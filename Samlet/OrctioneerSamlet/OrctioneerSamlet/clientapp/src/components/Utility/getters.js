export default {
    getAuctionById: (state) => (id) => {
        return state.auctions.find(auction => auction.ItemId === id)
    },
    getPopularAuctions: (state) => (amount) => {
        var auctions = state.auctions.slice();
        if (auctions != null) {
            return auctions.sort(function (aucA, aucB) {
                return aucB.Bids.length - aucA.Bids.length
            }).slice(0, amount)
        }
        else {
            return "error";
        }
    },
    getNewAuctions: (state) => (amount) => {
        var auctions = state.auctions.slice();
        if (auctions != null) {
            return auctions.sort(function (aucA, aucB) {
                return new Date(aucB.DateCreated) - new Date(aucA.DateCreated)
            }).slice(0, amount)
        }
    },
    getExpAuctions: (state) => (amount) =>{
        var auctions = state.auctions.slice();
        if (auctions != null) {
            return auctions.sort(function (aucA, aucB) {
                return new Date(aucA.ExpirationDate) - new Date(aucB.ExpirationDate)
            }).slice(0, amount)
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
    },
    getCurrentBids: (state) => {
        var bids = state.currentBids.slice()
        if (Array.isArray(bids) && bids.length) {
               return bids.sort(function (bidA, bidB) {
                    return bidB.Bid - bidA.Bid
                })
            }
            else {
                return bids = [{ Bid: 0 }]
            }
    }
}