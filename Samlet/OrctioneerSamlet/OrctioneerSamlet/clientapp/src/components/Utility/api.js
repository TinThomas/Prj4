import axios from "axios"
export default {
    //Get
    fetchAuctions() {
        return axios.get('http://localhost:5000/api/ItemEntity/item/')
    },
    fetchAuctionById(id) {
        return axios.get('http://localhost:5000/api/ItemEntity/item/' + id)
    },
    fetchBidsById(id) {
        return axios.get('http://localhost:5000/api/BidEntities/GetBidsFromItem/' + id)
    },

    //Post
    postImage(formData) {
        var ret = axios.post('http://localhost:5000/api/ItemEntity/CreateImage', formData)
        return ret;
    },

    postNewAuction(title, buyOutPrice, description, expDate, picturePath) {
        var ret =  axios.post('http://localhost:5000/api/ItemEntity/Item',
            {
                Title: title,
                BuyOutPrice: buyOutPrice,
                DescriptionOfItem: description,
                ExpirationDate: expDate,
                Image: picturePath
            })
        return ret;
    },

    postNewBid(bid, userIdBuyer, itemId) {
        var ret = axios.post('http://localhost:5000/api/BidEntities/newBid',
            {
                Bid: bid,
                UserIdBuyer: userIdBuyer,
                itemId: itemId
            })
        return ret;
    }
}
    