interface BuyerShipperInterface {
    OneWay:
        details( string )
}
interface BuyerSellerInterface {
    OneWay:
        quote1( int ),
        quote2( int )
}
