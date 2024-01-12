from BuyerShipperServiceInterfaceModule import BuyerInterface, ShipperInterface
from SellerServiceInterfaceModule import SellerBuyerInterface

include "console.iol"
service SellerService {

execution{ single }

outputPort Buyer {
    location: "socket://localhost:8000"
    protocol: http { format = "json" }
    interfaces: BuyerInterface
}

outputPort ShipperSeller {
    location: "socket://localhost:8001"
    protocol: http { format = "json" }
    interfaces: ShipperInterface
}

inputPort SellerBuyer {
    location: "socket://localhost:8002"
    protocol: http { format = "json" }
    interfaces: SellerBuyerInterface
}

main { 
    {[ask(product)]{
        quote@Buyer(17)
        println@Console( "Quoted" + product+ "for price 17")()
        {[accept(order)]
            println@Console( "Order accepted")()
            order@ShipperSeller(order)}
        
        {[reject(order)]}
            println@Console( "Order not accepted")()
        }
        }
    }
}