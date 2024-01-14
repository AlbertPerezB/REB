from BuyerShipperServiceInterfaceModule import BuyerInterface
from ShipperServiceInterfaceModule import ShipperSellerInterface

include "console.iol"
service ShipperService {

execution{ single }

outputPort Buyer {
    location: "socket://localhost:8005"
    protocol: http { format = "json" }
    interfaces: BuyerInterface
}

inputPort ShipperSeller {
    location: "socket://localhost:8003"
    protocol: http { format = "json" }
    interfaces: ShipperSellerInterface
}

main { 
    {[order(product)]{
        details@Buyer("invoice for " + product)
        println@Console( "Invoiced for " + product)()}
        }
    }
}