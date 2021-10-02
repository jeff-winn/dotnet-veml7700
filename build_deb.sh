#!/bin/sh
GITHUB_REF="refs/tags/0.0.1-rc1"
VERSION="$(echo $GITHUB_REF | sed -E 's,^refs/tags/,,')"
VERSION="$(echo $VERSION | sed -E 's/^v//')"

chmod -R 0755 dpkg-build/DEBIAN
sed -i 's/${VERSION}/'$VERSION'/g' 'dpkg-build/DEBIAN/control'
dpkg-deb -b dpkg-build 'dotnet-veml7700_'"$VERSION"'_armhf.deb'