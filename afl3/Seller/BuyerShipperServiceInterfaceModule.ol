interface BuyerInterface {
    OneWay:
        quote( int)
}

interface ShipperInterface {
    OneWay:
        order( string)
}
