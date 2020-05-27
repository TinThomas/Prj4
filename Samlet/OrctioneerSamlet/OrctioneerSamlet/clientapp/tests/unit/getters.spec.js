import getters from './../../src/components/Utility/getters'

test('getPopularAuctions returns correct amount', () => {
    const auctions = Array(30)
        .fill()

    const state = {
        auctions,
    }
    const amount = 4;

    const result = getters.getPopularAuctions(state)(amount);
    expect(result).toEqual(auctions.slice(0,4))
})

test('getNewAuctions returns correct amount', () => {
    const auctions = Array(30)
        .fill()

    const state = {
        auctions,
    }
    const amount = 20;

    const result = getters.getNewAuctions(state)(amount);
    expect(result).toEqual(auctions.slice(0, 20))
})

test('getExpAuctions returns correct amount', () => {
    const auctions = Array(30)
        .fill()

    const state = {
        auctions,
    }
    const amount = 15;

    const result = getters.getNewAuctions(state)(amount);
    expect(result).toEqual(auctions.slice(0, 15))
})


test('getHighestBid returns highest bid', () => {
    const bids = [
        { Bid: 10 },
        { Bid: 20 },
        { Bid: 30 },
    ]
    const state = {
        bids,
    }

    const result = getters.getHighestBid()(bids);
    expect(result).toEqual(30 + " Gold")

})

test('getHighestBid returns no bids', () => {
    const bids = [
        
    ]
    const state = {
        bids,
    }

    const result = getters.getHighestBid()(bids);
    expect(result).toEqual("No bids");
})

test('getCurrentHighestBid returns highest bid in currentBids', () => {
    const currentBids = [
        { Bid: 30 },
        { Bid: 20 },
        { Bid: 10 },
    ]
    const state = {
        currentBids,
    }

    const result = getters.getCurrentHighestBid(state);
    expect(result).toEqual(currentBids[0].Bid + " Gold");
})

test('getCurrentBids (In order) returns no bids', () => {
    const currentBids = [
        { Bid: 10 },
        { Bid: 20 },
        { Bid: 30 },
    ]
    const state = {
        currentBids,
    }

    const sortedBids = [
        { Bid: 30 },
        { Bid: 20 },
        { Bid: 10 },
    ]

    const result = getters.getCurrentBids(state);
    expect(result).toEqual(sortedBids);
})




