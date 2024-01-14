interface BuyerInterface {
    OneWay:
        quote1( int ),
        quote2( int )
}

interface ShipperInterface {
    OneWay:
        order( string )
}
