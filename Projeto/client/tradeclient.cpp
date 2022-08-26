#ifdef _MSC_VER
#pragma warning( disable : 4503 4355 4786 )
#else
#include "config.h"
#endif

#include "quickfix/FileStore.h"
#include "quickfix/SocketInitiator.h"
#ifdef HAVE_SSL
#include "quickfix/ThreadedSSLSocketInitiator.h"
#include "quickfix/SSLSocketInitiator.h"
#endif
#include "quickfix/SessionSettings.h"
#include "quickfix/Log.h"
#include "Application.h"
#include <string>
#include <iostream>
#include <fstream>


int main( int argc, char** argv )
{
  if ( argc < 2 )
  {
    std::cout << "usage: " << argv[ 0 ]
    << " FILE." << std::endl;
    return 0;
  }
  std::string file = argv[ 1 ];

#ifdef HAVE_SSL
  std::string isSSL;
  if (argc > 2)
  {
    isSSL.assign(argv[2]);
  }
#endif

  FIX::Initiator * initiator = 0;
  try
  {
    FIX::SessionSettings settings(file);

    Application application;
    FIX::FileStoreFactory storeFactory(settings);
    FIX::ScreenLogFactory logFactory(settings);
#ifdef HAVE_SSL
    if (isSSL.compare("SSL") == 0)
      initiator = new FIX::ThreadedSSLSocketInitiator ( application, storeFactory, settings, logFactory );
    else if (isSSL.compare("SSL-ST") == 0)
      initiator = new FIX::SSLSocketInitiator ( application, storeFactory, settings, logFactory );
    else
#endif
    initiator = new FIX::SocketInitiator( application, storeFactory, settings, logFactory );

    initiator->start();
    application.run();
    initiator->stop();
    delete initiator;

    return 0;
  }
  catch ( std::exception & e )
  {
    std::cout << e.what();
    delete initiator;
    return 1;
  }
}
