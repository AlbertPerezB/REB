from BuyerShipperServiceInterfaceModule import BuyerInterface
from ShipperServiceInterfaceModule import ShipperSellerInterface

include "console.iol"
service ShipperService {

execution{ single }

outputPort Buyer {
    location: "socket://localhost:8000"
    protocol: http { format = "json" },
    interfaces: BuyerInterface
}

inputPort ShipperSeller {
    location: "socket://localhost:8001"
    protocol: http { format = "json" }
    interfaces: ShipperSellerInterface
}

main { 
    {[ask2sell(product)]{
        quote@Buyer(17)
        println@Console( "Quoted" + product+ "for price 17")()
        {[accept(order)]
            println@Console( "Order accepted")()
            order@Shipper(order)}
        
        {[reject(order)]}
            println@Console( "Order not accepted")()
        }
        }
    }
}