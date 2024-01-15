from SellerShipperServiceInterfaceModule import SellerInterface
from BuyerServiceInterfaceModule import BuyerShipperInterface, BuyerSellerInterface
from time import Time

include "console.iol"

service BuyerService {

execution{ single }
embed Time as Time

outputPort Seller1 {
    location: "socket://localhost:8004"
    protocol: http { format = "json" }
    interfaces: SellerInterface
}

outputPort Seller2 {
    location: "socket://localhost:8002"
    protocol: http { format = "json" }
    interfaces: SellerInterface
}

inputPort ShipperBuyer {
    location: "socket://localhost:8005"
    protocol: http { format = "json" }
    interfaces: BuyerShipperInterface
}

inputPort SellerBuyer {
    location: "socket://localhost:8006"
    protocol: http { format = "json" }
    interfaces: BuyerSellerInterface
}

main {
    /* Ask for quotes from both Seller1 and Seller2 */
    ask@Seller1("chips") ; [quote1(price1)]{ println@Console("Received 1 quote")()} |
    ask@Seller2("chips") ; [quote2(price2)]{ println@Console("Received 2 quote")()} 
    
    println@Console("Received both quotes")()
    
    if (price1 < 21 || price2 < 21) {
        if (price1 < price2) {
            accept@Seller1("Ok to buy chips for " + price1) |
            reject@Seller2("Not ok to buy chips for " + price2) ;
            println@Console("Accepted quote from seller 1 for " + price1)()
        } else {
            accept@Seller2("Ok to buy chips for " + price2) |
            reject@Seller1("Not ok to buy chips for " + price1) ;
            println@Console("Accepted quote from seller 2 for " + price2)()
        }
        [details(invoice)]
        println@Console( "Received "+ invoice +" from Shipper!")()

    } else {
        println@Console( "Prices not lower than 20")() ;
        reject@Seller1("Not ok to buy chips for " + price1) |
        reject@Seller2("Not ok to buy chips for " + price2)
        }
    }
}
    
