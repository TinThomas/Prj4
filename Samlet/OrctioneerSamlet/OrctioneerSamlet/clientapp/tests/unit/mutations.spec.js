import mutations from './../../src/components/Utility/mutations'
import mockData from './mockData'


//Action lists
test('correctly save Auctions', () => {
    const state = {
        auctions: []
    }
    //Assign mock data
    const auctions = mockData.AUCTIONS;
    //Act: Call with state object and payload
    mutations.SAVE_AUCTIONS(state, auctions)
    //Assert state value updated
    expect(state.auctions).toBe(auctions)
})

test('correctly save popAuctions', () => {
    const state = {
        popAuctions: []
    }
    //Assign mock data
    const popAuctions = mockData.AUCTIONS;
    //Call with state object and payload
    mutations.SAVE_POP_AUCTIONS(state, popAuctions)
    //Assert state value updated
    expect(state.popAuctions).toBe(popAuctions)
})

test('correctly save newAuctions', () => {
    const state = {
        newAuctions: []
    }
    //Assign mock data
    const newAuctions = mockData.AUCTIONS;
    //Call with state object and payload
    mutations.SAVE_NEW_AUCTIONS(state, newAuctions)
    //Assert state value updated
    expect(state.newAuctions).toBe(newAuctions)
})

test('correctly save expAuctions', () => {
    const state = {
        expAuctions: []
    }
    //Assign mock data
    const expAuctions = mockData.AUCTIONS;
    //Call with state object and payload
    mutations.SAVE_EXP_AUCTIONS(state, expAuctions)
    //Assert state value updated
    expect(state.expAuctions).toBe(expAuctions)
})

test('correctly update listing component', () => {
    const state = {
        updateListing: 0
    }
    //Assign mock data
    const updateListing = 1
    //Call with state object and payload
    mutations.UPDATE_LISTING(state)
    //Assert state value updated
    expect(state.updateListing).toBe(updateListing)
})

//Add auction
test('correctly save new picture on upload', () => {
    const state = {
        picturePath: ""
    }
    //Assign mock data
    const picturePath = mockData.PICTURE
    //Call with state object and payload
    mutations.SAVE_NEW_PICTURE(state, picturePath)
    //Assert state value updated
    expect(state.picturePath).toBe(picturePath)
})

test('upload succesfully', () => {
    const state = {
        auctionSubmitted: false
    }
    //Assign mock data
    const auctionSubmitted = true
    //Call with state object and payload 
    mutations.UPLOAD_SUCCES(state, auctionSubmitted)
    //Assert state value updated
    expect(state.auctionSubmitted).toBe(auctionSubmitted)
})

//Current auction
test('correctly save currentAuction', () => {
    const state = {
        currentAuction: []
    }
    //Assign mock data
    const currentAuction = mockData.currentAUCTION
    //Call with state object and payload 
    mutations.SAVE_CURRENT_AUCTION(state, currentAuction)
    //Assert state value updated
    expect(state.currentAuction).toBe(currentAuction)
})

test('correctly save current picture url', () => {
    const state = {
        currentPictureUrl: ""
    }
    //Assign mock data
    const currentPictureUrl = mockData.currentPICTUREURL
    //Call with state object and payload 
    mutations.SAVE_CURRENT_PICTURE_URL(state, currentPictureUrl)
    //Assert state value updated
    expect(state.currentPictureUrl).toBe(currentPictureUrl)
})

test('correctly save current bids', () => {
    const state = {
        currentBids: []
    }
    //Assign mock data
    const currentBids = mockData.currentBIDS
    //Call with state object and payload 
    mutations.SAVE_CURRENT_BIDS(state, currentBids)
    //Assert state value updated
    expect(state.currentBids).toBe(currentBids)
})

test('succes on bid post', () => {
    const state = {
        bidSubmitted: false,
        currentBidTableKey: 0
    }
    //Assign mock data
    const bidSubmitted = true
    const currentBidTableKey = 1
    //Call with state object and payload 
    mutations.BID_SUCCES(state, bidSubmitted)
    //Assert state value updated
    expect(state.bidSubmitted).toBe(bidSubmitted)
    expect(state.currentBidTableKey).toBe(currentBidTableKey)
})


//Search
test('correctly update search word', () => {
    const state = {
        search: "",
        UpdateSortedAuctions: 0
    }
    //Assign mock data
    const search = mockData.SEARCHWORD
    const UpdateSortedAuctions = 1
    //Call with state object and payload 
    mutations.UPDATE_SEARCH_WORD(state, search)
    //Assert state value updated
    expect(state.search).toBe(search)
    expect(state.UpdateSortedAuctions).toBe(UpdateSortedAuctions)
})

