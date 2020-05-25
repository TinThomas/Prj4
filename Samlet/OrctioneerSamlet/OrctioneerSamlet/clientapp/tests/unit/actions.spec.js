import actions from './../../src/components/Utility/actions'
import api from './../../src/components/Utility/api'
import Photo from './../../src/images/Deluxe_bow.jpg'

function FormDataMock() {
    this.append = jest.fn();
}
global.FormData = FormDataMock

jest.mock('./../../src/components/Utility/api')

//Auction lists
test('loadAuction commits data returned by api method', async () => {
    api.fetchAuctions.mockClear()
    const response = {
        data: [1, 2, 3],
        status: 200
    }

    api.fetchAuctions.mockResolvedValue(response)
    const commit = jest.fn()
    await actions.loadAuctions({ commit })
    
    expect(commit).toHaveBeenCalledWith('SAVE_AUCTIONS', response.data);
    expect(commit).toHaveBeenCalledWith('UPDATE_LISTING');
    expect(commit).toHaveBeenCalledTimes(2);
})

test('updateListing commits UPDATE_LISTING', () => {
    const commit = jest.fn()

    actions.updateListing({ commit });

    expect(commit).toHaveBeenCalledWith('UPDATE_LISTING');
    expect(commit).toHaveBeenCalledTimes(1);
})

//Add auction
test('uploadFile commits data returned by api post method',async () => {
    const commit = jest.fn();
    const formData = new FormData();
    const payload = {
        selectedFile: Photo,
        formData: formData
    }

    const response = {
        data: [
            Photo
        ],
        status: 200
    }

    api.postImage.mockResolvedValue(response);

    await actions.uploadFile({ commit }, payload);
    expect(commit).toHaveBeenCalledWith('SAVE_NEW_PICTURE', response.data);
    //Dunno om den mangler at tjekke hvad body egentlig er
})

test('postNewAuction commits true', async () => {
    const commit = jest.fn();
    const payload = {
        title: "Sword",
        buyOutPrice: 123,
        description: "Cool",
        expDate: "0001-01-01T00:00:00",
        picturePath: Photo
    }

    const response = {
        data: [
        ], 
        status: 200
    }

    api.postNewAuction.mockResolvedValue(response);

    await actions.postNewAuction({ commit }, payload);
    expect(commit).toHaveBeenCalledWith('UPLOAD_SUCCES', true);
    //Dunno om den mangler at tjekke hvad body egentlig er
})

test('postNewAuction commits false', async () => {
    const commit = jest.fn();
    const payload = {
        title: "Sword",
        buyOutPrice: 123,
        description: "Cool",
        expDate: "0001-01-01T00:00:00",
        picturePath: Photo
    }

    const response = {
        data: [
        ],
        status: 201
    }

    api.postNewAuction.mockResolvedValue(response);

    await actions.postNewAuction({ commit }, payload);
    expect(commit).toHaveBeenCalledWith('UPLOAD_SUCCES', false);
    //Dunno om den mangler at tjekke hvad body egentlig er
})

//Add bid
test('postNewBid commits true', async () => {
    const commit = jest.fn();
    const payload = {
        bid: 100,
        userIdBuyer: "Jacob",
        itemId: 2
    }

    const response = {
        data: [
        ],
        status: 200
    }

    api.postNewBid.mockResolvedValue(response);

    await actions.postNewBid({ commit }, payload);
    expect(commit).toHaveBeenCalledWith('BID_SUCCES', true);
    //Dunno om den mangler at tjekke hvad body egentlig er
})

test('postNewBid commits true', async () => {
    const commit = jest.fn();
    const payload = {
        bid: 100,
        userIdBuyer: "Jacob",
        itemId: 2
    }

    const response = {
        data: [
        ],
        status: 201
    }

    api.postNewBid.mockResolvedValue(response);

    await actions.postNewBid({ commit }, payload);
    expect(commit).toHaveBeenCalledWith('BID_SUCCES', false);
    //Dunno om den mangler at tjekke hvad body egentlig er
})

//Current auction
test('loadCurrentAuction commits data returned by api method', async () => {
    const response = {
        data: [{
            ItemId: 1,
            Title: "Elven Bow BUY NOW",
            BuyOutPrice: 5000,
            ExpirationDate: "2020-12-24T00:00:00",
            Image: Photo
            }],
        status: 200
    }
    const payload = {
        id: 1
    }

    api.fetchAuctionById.mockResolvedValue(response)
    const commit = jest.fn()
    await actions.loadCurrentAuction({ commit }, payload)

    expect(commit).toHaveBeenCalledWith('SAVE_CURRENT_AUCTION', response.data);
    expect(commit).toHaveBeenCalledWith('SAVE_CURRENT_PICTURE_URL', Photo );
    expect(commit).toHaveBeenCalledTimes(2);
})

test('loadCurrentBids commits data returned by api method', async () => {
    const response = {
        data: [{
            Id: 1,
            Bid: 200,
            UserIdBuyer: "30",
            Item: null,
            ItemId: 1,
            Created: "0001-01-01T00:00:00"
        }],
        status: 200
    }
    const payload = {
        id: 1
    }

    api.fetchBidsById.mockResolvedValue(response);
    const commit = jest.fn()
    await actions.loadCurrentBids({ commit }, payload)

    expect(commit).toHaveBeenCalledWith('SAVE_CURRENT_BIDS', response.data);
    expect(commit).toHaveBeenCalledTimes(1);
})

test('resetBidSubmitted commits BID_SUCCES false', () => {
    const commit = jest.fn()

    actions.resetBidSubmitted({ commit });

    expect(commit).toHaveBeenCalledWith('BID_SUCCES', false);
    expect(commit).toHaveBeenCalledTimes(1);
})

//Search
test('updateSearchWord commits UPDATE_SEARCH_WORD word', () => {
    const word = "sword"

    const commit = jest.fn()

    actions.updateSearchWord({ commit }, word);

    expect(commit).toHaveBeenCalledWith('UPDATE_SEARCH_WORD', word);
    expect(commit).toHaveBeenCalledTimes(1);
})

test('updateSortBy commits UPDATE_SORT_BY sort', () => {
    const sort = "Popularity"

    const commit = jest.fn()

    actions.updateSortBy({ commit }, sort);

    expect(commit).toHaveBeenCalledWith('UPDATE_SORT_BY', sort);
    expect(commit).toHaveBeenCalledTimes(1);
})