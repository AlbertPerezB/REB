from BuyerShipperServiceInterfaceModule import BuyerInterface, ShipperInterface
from SellerServiceInterfaceModule import SellerBuyerInterface

include "console.iol"
service SellerService {

execution{ single }

outputPort Buyer {
    location: "socket://localhost:8006"
    protocol: http { format = "json" }
    interfaces: BuyerInterface
}

outputPort ShipperSeller {
    location: "socket://localhost:8003"
    protocol: http { format = "json" }
    interfaces: ShipperInterface
}

inputPort SellerBuyer {
    location: "socket://localhost:8004"
    protocol: http { format = "json" }
    interfaces: SellerBuyerInterface
}

main { 
    {[ask(product)]{
        quote1@Buyer(17)
        println@Console( "Quoted " + product+ " for price 17")()
        [accept(order)]{
            println@Console( "Order accepted")()
            order@ShipperSeller(product)}
        |
        [reject(order)]{ 
            println@Console( "Order not accepted")()}
        }    
        
    }
}}