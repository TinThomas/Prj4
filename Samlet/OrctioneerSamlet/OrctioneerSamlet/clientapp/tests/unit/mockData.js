export default {

    get AUCTIONS() {
        return [
            { Title: "Sword", Description: "Cool", Id: 1, CurrentBid: 100 },
            { Title: "Hammer", Description: "Nice", Id: 2, CurrentBid: 200 },
            { Title: "Axe", Description: "Awesine", Id: 3, CurrentBid: 300 },
        ]   
    },

    get PICTURE() {
        return "./../../images/sværd.jpg"
    },

    get currentAUCTION() {
        return { Title: "Sword", Description: "Cool", Id: 1, CurrentBid: 100 }
    },

    get currentPICTUREURL() {
        return "./../../images/Hjelm_og_Brynje.jpg"
    },

    get currentBIDS() {
        return [
            { Id: 1, Bid: 200, UserIdBuyer: "31", },
            { Id: 2, Bid: 300, UserIdBuyer: "32", },
            { Id: 3, Bid: 400, UserIdBuyer: "33", },
        ]
    },

    get SEARCHWORD() {
        return "BOW"
    },

    get TEST_AUCTION() {
        return {
            title: "test",
            buyOutPrice: 123,
            description: "super",
            expDate: "0001-01 - 01T00: 00: 00",
            picturePath: "xd.jpg"
        }
    }


}